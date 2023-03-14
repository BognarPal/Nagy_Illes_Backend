import mysql.connector
import random
import secrets
import hashlib
import string
import datetime
import time

mydb = mysql.connector.connect(host="localhost", user="root", password="", database="project_discite")

mycursor = mydb.cursor()
user_id = 0
run_id = 0

#user
for _ in range(1, 100):
    user_id += 1
    _username = ''.join(random.choices(string.ascii_letters, k=random.randint(5, 15)))
    _email = f"{_username}@{''.join(random.choices(string.ascii_lowercase, k=5))}mail.{random.choice(['com', 'hu', 'ro'])}"
    _password = ''.join(random.choices(string.ascii_letters + string.digits + string.punctuation, k=random.randint(8, 16)))
    _salt = secrets.token_bytes(16)
    _hash = hashlib.sha256(_password.encode("utf-8") + _salt).digest()
    _regdate = random.uniform(1650000000.0, time.time())
    _lastactive = random.uniform(_regdate, time.time())
    sql = "INSERT INTO user (id, username, email, hash, salt, register_date, lastactive) VALUES (%s, %s, %s, %s, %s, %s, %s)"
    val = (user_id, _username, _email, _hash, _salt, datetime.datetime.fromtimestamp(_regdate).strftime('%Y-%m-%d %H:%M:%S'), datetime.datetime.fromtimestamp(_lastactive).strftime('%Y-%m-%d %H:%M:%S'))
    mycursor.execute(sql, val)

    #user_class
    classes = [2]
    classes += random.choice(([1, 3], [1]))
    for _class in classes:
        sql = "INSERT INTO user_class (UserId, ClassId) VALUES (%s, %s)"
        val = (user_id, _class)
        mycursor.execute(sql, val)
    
    #run
    for __ in range(1, random.randint(1, 10)):
        run_id += 1
        _seed = random.randint(0, 2147483647)
        _gold = random.randint(0, 1000)
        _progress = random.randint(0, 2)
        _currenthp = random.randint(1, 100) if _progress != 0 else 0
        _score = random.randint(100, 10000)
        _path = ''.join(random.choices("URDL", k=random.randint(0, 40)))
        _runtime = random.randint(0, 10000)
        _startdate = random.uniform(1650000000.0, time.time())
        _enddate = random.uniform(_startdate, time.time()) if _progress in (0, 2) else 0
        sql = "INSERT INTO run (id, `UserId`, ClassId, path, seed, gold, score, runtime, Status, version, startdate, enddate, currenthp) VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"
        val = (run_id, user_id, random.choice(classes), _path, _seed, _gold, _score, _runtime, _progress, "1.0.0", datetime.datetime.fromtimestamp(_startdate).strftime('%Y-%m-%d %H:%M:%S'), datetime.datetime.fromtimestamp(_enddate).strftime('%Y-%m-%d %H:%M:%S'), _currenthp)
        mycursor.execute(sql, val)

        #run_weapon
        for weapon in range(1, 6):
            sql = "INSERT INTO `run_weapon`(`RunId`, `WeaponId`, `Picked`, `seen`) VALUES (%s, %s, %s, %s);"
            val = (run_id, weapon, random.randint(1, 5), random.randint(1, 5))
            mycursor.execute(sql, val)

        #run_artifact
        for artifact in range(1, 5):
            sql = "INSERT INTO `run_weapon`(`RunId`, `WeaponId`, `Picked`, `seen`) VALUES (%s, %s, %s, %s);"
            val = (run_id, weapon, random.randint(1, 5), random.randint(1, 5))
            mycursor.execute(sql, val)

        #run_enemy
        for enemy in range(1, 7):
            sql = "INSERT INTO `run_enemy`(`RunId`, `EnemyId`, `deaths`, `seen`, `Damage`) VALUES (%s, %s, %s, %s, %s)"
            val = (run_id, enemy, random.randint(1, 50), random.randint(1, 50), random.randint(1, 1000))
            mycursor.execute(sql, val)


mydb.commit()
