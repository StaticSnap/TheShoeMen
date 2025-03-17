from flask_cors import CORS, cross_origin
from flask import Flask, redirect, request, send_file, jsonify
import json
import sqlite3

con = sqlite3.connect("tutorial.db", check_same_thread=False)
cur = con.cursor()

app = Flask(__name__)
cors = CORS(app)
app.config['CORS_HEADERS'] = 'Content-Type'

@app.route('/',methods=["POST"])
@cross_origin()
def root():
	global con
	query = request.get_data().decode()
	print(query)
	result = cur.execute(query)

	columns = [description[0] for description in cur.description]

	rows = result.fetchall()

	formattedResult = [
		dict(zip(columns, row)) for row in rows
	]

	con.commit()
	return jsonify(formattedResult)

app.run(port=8080, host="0.0.0.0")