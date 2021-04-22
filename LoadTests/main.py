from locust import HttpUser, between, task, log
from locust.clients import HttpSession
import json
import logging

opened = False


class GridUser(HttpUser):
    wait_time = between(5, 15)
    host = "http://localhost:49481"
    server_host = "http://localhost:49429"

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)

        self.token = ""
        self.headers = {}

    def on_start(self):
        self.token = self.login()

        self.headers = {'Authorization': 'Token ' + self.token}

    def login(self):
        response = self.client.post(GridUser.server_host + "/auth/login", json={
                'email': 'test2@email.com',
                'password': 'testPassword1'
        })

        return response.json()['Token']

    @task
    def index(self):
        self.client.get("/")
        self.client.get("/_framework/blazor.webassembly.js")
        self.client.get("/_framework/blazor.boot.json")
        self.client.get("/css/open-iconic/font/css/open-iconic-bootstrap.min.css")
        self.client.get("/css/open-iconic/font/fonts/open-iconic.woff")
        self.client.get("/favicon.ico")
        self.client.get("/_framework/dotnet.5.0.4.js")
        self.client.get("/css/electric_grid.gif")

    @task
    def login_t(self):
        self.client.get("/login")

    @task
    def grid(self):
        self.client.get("/grid", headers=self.headers)

    @task
    def control_panel(self):
        self.client.get("/control_panel", headers=self.headers)


if __name__ == "__main__":
    from locust.env import Environment
    my_env = Environment(user_classes=[GridUser])
    GridUser(my_env).run()