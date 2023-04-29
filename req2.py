import requests


url = 'http://localhost:8000/chat'
prompt = 'character: rabbit \n hi I am lola' 

# Set the query parameter
params = {'text': prompt}

# Send the GET request
response = requests.get(url, params=params)

# Check the response status code
if response.status_code == 200:
    data = response.json()
    print(data)
else:
    # Request failed
    print(f"Request failed with status code: {response.status_code}")

