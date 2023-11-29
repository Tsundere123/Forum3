import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Home } from "../models/home.model";

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) { }

  getData() {
    return this.http.get<Home>('api/Home/');
  }
}
