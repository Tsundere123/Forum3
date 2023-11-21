import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {Category} from "../models/category.model";

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  // baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }
  getAllCategories():Observable<Category[]>{
    return this.http.get<Category[]>( "api/Category")
  }
}
