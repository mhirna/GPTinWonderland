import requests


url = 'http://localhost:8000/text_to_3d'
prompt = 'cat' 

# Set the query parameter
params = {'prompt': prompt}

# Send the GET request
response = requests.get(url, params=params)

# Check the response status code
if response.status_code == 200:
    data = response.json()
    print(data)
else:
    # Request failed
    print(f"Request failed with status code: {response.status_code}")
