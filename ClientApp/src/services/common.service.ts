import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../constants';

import { map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';
import { UserInfo } from 'src/models/userInfo';
import * as jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  session;
  header;
  token;
  httpOptions;
  userData = new BehaviorSubject<UserInfo>(new UserInfo());

  constructor(private http: HttpClient) {

    //for POST purpose
    this.header = new HttpHeaders().set("Content-Type", "application/json");
    this.header.set("Accept", "application/json");



    this.session = sessionStorage.getItem(
      'oidc.user:https://localhost:44358:mgSolutions'
    );
    this.token = JSON.parse(this.session);

    if(this.token){
      this.httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          Accept: 'application/json',
          Authorization: "Bearer " + this.token['access_token'],
        }),
      };
    }
    

  }

  fetchData() {
    

    //console.log(this.token['access_token']);

    //return this.http.get(Constants.apiRoot + '/api/user', httpOptions);

    return this.http
    .get<any>(Constants.apiRoot + '/api/info')
    .pipe(
      map((res) => {
        return res;
      })
    );
  }

  getInfoById(id) {
    return this.http
      .get<any>(Constants.apiRoot + '/api/info/' + id)
      .pipe(
        map((res) => {
          return res;
        })
      );
  }


  getUserDetails(){
    if (this.token['access_token']) {
      const userDetails = new UserInfo();
      
      const decodeUserDetails = jwt_decode(this.token["access_token"]);
      userDetails.userId =
        decodeUserDetails[
          "sub"
        ];
      userDetails.role =
        decodeUserDetails[
          "role"
        ];

      this.userData.next(userDetails);
      //console.log(this.userData);

    }
  }


  saveMessage(saveMessageObj: any) {
    return this.http
      .post<any>(
        Constants.apiRoot + '/api/info/create',
        JSON.stringify(saveMessageObj),
        { headers: this.header }
      )
      .pipe(
        map(res => {
          return res;
        })
      );
  }

  updateMessage(updateMessageObj: any, id) {
    return this.http
      .post<any>(
        Constants.apiRoot + '/api/info/update/' + id,
        JSON.stringify(updateMessageObj),
        { headers: this.header }
      )
      .pipe(
        map((res) => {
          return res;
        })
      );
  }



  
}


