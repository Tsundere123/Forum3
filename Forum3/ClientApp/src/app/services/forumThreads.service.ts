import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {Category} from "../models/forumCategory.model";
import {Thread} from "../models/forumThread.model";
@Injectable({
  providedIn: 'root'
})
export class ForumThreadsService {

  constructor(private http: HttpClient) { }
  // getCategoryName():Observable<Category[]>{
  //   return this.http.get<Category[]>("api/ForumCategory/:id")
  // }
  getAllThreadsOfCategory(id:number):Observable<Thread[]>{
    return this.http.get<Thread[]>("api/ForumThread/" + id)
  }
}
