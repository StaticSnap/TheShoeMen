from flask import Flask, request, jsonify
from flask_cors import CORS
import sqlite3
import json

DATABASE = "tutorial.db"

def create_app():
    app = Flask(__name__)
    CORS(app)

    def get_db():
        conn = sqlite3.connect(DATABASE)
        conn.row_factory = sqlite3.Row
        conn.execute("PRAGMA journal_mode=WAL;")
        return conn

    def execute_safe_query(query_type, parameters=None):
        """Execute a safe parameterized query based on the query type"""
        try:
            with get_db() as conn:
                cur = conn.cursor()
                
                if query_type == "select":
                    if "users" in parameters.get("table", ""):
                        cur.execute("SELECT * FROM users WHERE username = ? AND password = ?", 
                                  (parameters.get("username"), parameters.get("password")))
                    elif "shoe" in parameters.get("table", ""):
                        search_term = f"%{parameters.get('search_term', '')}%"
                        cur.execute("SELECT * FROM shoe WHERE model = ? OR manufacturer LIKE ?", 
                                  (parameters.get("search_term"), search_term))
                        # Debug: Print the query and results
                        print(f"Executing shoe query with search term: {search_term}")
                    else:
                        cur.execute("SELECT * FROM ?", (parameters.get("table"),))
                
                elif query_type == "insert":
                    if "users" in parameters.get("table", ""):
                        cur.execute("INSERT INTO users (username, password, user_role) VALUES (?, ?, ?)",
                                  (parameters.get("username"), parameters.get("password"), parameters.get("role")))
                    elif "reviews" in parameters.get("table", ""):
                        cur.execute("INSERT INTO reviews (username, shoe_id, rating) VALUES (?, ?, ?)",
                                  (parameters.get("username"), parameters.get("shoe_id"), parameters.get("rating")))
                
                conn.commit()
                
                if cur.description:
                    rows = cur.fetchall()
                    columns = [col[0] for col in cur.description]
                    result = [dict(zip(columns, row)) for row in rows]
                    # Debug: Print the results
                    print(f"Query results: {json.dumps(result, indent=2)}")
                    
                    # Ensure imagePath is included in the response
                    for row in result:
                        if 'imagePath' not in row:
                            row['imagePath'] = f"images/{row['manufacturer'].lower()}-{row['model'].lower().replace(' ', '')}.png"
                    
                    return result
                return {"message": "Query executed successfully"}
                
        except sqlite3.Error as e:
            return {"error": str(e)}

    @app.route('/api/query', methods=["POST"])
    def handle_query():
        try:
            data = request.get_json()
            if not data or "type" not in data:
                return jsonify({"error": "Invalid request format"}), 400

            query_type = data.get("type")
            parameters = data.get("parameters", {})

            # Debug: Print incoming request
            print(f"Received query: type={query_type}, parameters={json.dumps(parameters, indent=2)}")

            # Input validation
            if query_type not in ["select", "insert"]:
                return jsonify({"error": "Invalid query type"}), 400

            result = execute_safe_query(query_type, parameters)
            if "error" in result:
                return jsonify(result), 500
            return jsonify(result)

        except Exception as e:
            print(f"Error processing query: {str(e)}")
            return jsonify({"error": str(e)}), 500

    @app.route('/api/update-shoe-images', methods=["POST"])
    def update_shoe_images():
        try:
            with get_db() as conn:
                cur = conn.cursor()
                
                # Update Vans Old-Skool
                cur.execute("UPDATE shoe SET imagePath = ? WHERE model = ? AND manufacturer = ?",
                          ("images/vans-oldskool.png", "Old-Skool", "Vans"))
                
                # Update New Balance 574
                cur.execute("UPDATE shoe SET imagePath = ? WHERE model = ? AND manufacturer = ?",
                          ("images/newbalance-574.png", "574", "New Balance"))
                
                # Update Nike HD-Blue
                cur.execute("UPDATE shoe SET imagePath = ? WHERE model = ? AND manufacturer = ?",
                          ("images/nike-hdblue.png", "HD-Blue", "Nike"))
                
                conn.commit()
                return jsonify({"message": "Shoe images updated successfully"})
                
        except Exception as e:
            return jsonify({"error": str(e)}), 500

    return app

if __name__ == "__main__":
    app = create_app()
    app.run(debug=True, host="0.0.0.0", port=8080)
