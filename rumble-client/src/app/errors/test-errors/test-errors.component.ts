import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.scss'],
})

//Just for development!! must be removed in Production
export class TestErrorsComponent implements OnInit {
  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  get400Error() {
    this.http.get(`${this.baseUrl}/bugs/bad-request`).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  get404Error() {
    this.http.get(`${this.baseUrl}/bugs/not-found`).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  get401Error() {
    this.http.get(`${this.baseUrl}/bugs/auth`).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  get500Error() {
    this.http.get(`${this.baseUrl}/bugs/server-error`).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  get400ValidationError() {
    this.http.post(`${this.baseUrl}/account/register`, {}).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
