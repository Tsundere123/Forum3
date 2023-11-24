import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ForumCategory} from "../models/forumCategory.model";
import {ForumThread} from "../models/forumThread.model";
@Injectable({
  providedIn: 'root'
})
export class ForumThreadsService {

  constructor(private http: HttpClient) { }
  // getCategoryName():Observable<Category[]>{
  //   return this.http.get<Category[]>("api/ForumCategory/:id")
  // }
  getAllThreadsOfCategory(id:number):Observable<ForumThread[]>{
    return this.http.get<ForumThread[]>("api/ForumThread/" + id)
  }
}
