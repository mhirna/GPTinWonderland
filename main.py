from fastapi import FastAPI
from fastapi import HTTPException
import requests
import json
import numpy as np
import open3d as o3d
import humanloop as hl


HUMANLOOP_API_KEY = "hl_sk_4d28d90f8ada7c9d7bf0e12f7085117d4f264e31062b4449"

hl.init(
    api_key=HUMANLOOP_API_KEY,
    provider_api_keys={
        "openai": "OPENAI_KEY_HERE"
    }
)

app = FastAPI()

def convert_string_to_rgb(x):
    return x[4:-1].split(',')

@app.get("/text_to_3d")
async def convert_text_to_object(prompt: str):
    try:
        print("recieved prompt: ", prompt)
        response = requests.post("https://openai-point-e.hf.space/run/predict", json={
          "data": [
            prompt,
        ]}).json()
        data = response["data"]
        json_acceptable_string = data[0]['plot'].replace("'", "\"")
        d = json.loads(json_acceptable_string)
        c = d['data'][0]['marker']['color']
        rgb = np.array(list(map(convert_string_to_rgb, c))).astype(np.float32)
        xyz = np.stack((d['data'][0]['x'], d['data'][0]['y'], d['data'][0]['z']), axis=1)
        pcd = o3d.geometry.PointCloud()
        pcd.points = o3d.utility.Vector3dVector(xyz)
        pcd.colors = o3d.utility.Vector3dVector(rgb)

        # Estimate normals for the point cloud
        pcd.estimate_normals(search_param=o3d.geometry.KDTreeSearchParamHybrid(radius=0.5, max_nn=30))
        radii = [0.05, 0.01, 0.02]

        mesh = o3d.geometry.TriangleMesh.create_from_point_cloud_ball_pivoting(
            pcd,
            o3d.utility.DoubleVector(radii)
        )
        #mesh = mesh.filter_smooth_taubin(number_of_iterations=10)  # Example of mesh smoothing
        #mesh = mesh.filter_laplacian(subdivision=3)  # Example of mesh subdivision
        o3d.io.write_triangle_mesh('output_mesh.obj', mesh)
        print("Generated mesh!")

        with open('output_mesh.obj', "rb") as f:
            object_file_data = f.read()

        # Set the appropriate response headers
        headers = {
            "Content-Disposition": "attachment; filename=object.obj",
            "Content-Type": "application/octet-stream",
        }

        # Return the object file data in the response
        return {"success": True}

    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

@app.get("/chat")
async def chat(text: str):

    url = "https://api.humanloop.com/v4/chat-deployed"

    payload = {
        "messages": [
            {
                "content": text,
                "role": "user"
            }
        ],
        "num_samples": 1,
        "stream": False,
        "project": "GPTinWonderland"
    }
    headers = {
        "accept": "application/json",
        "content-type": "application/json",
        "X-API-KEY": HUMANLOOP_API_KEY
    }

    response = requests.post(url, json=payload, headers=headers).json()
    out = response["data"][0]["output"]
    return {"output": out}
    


