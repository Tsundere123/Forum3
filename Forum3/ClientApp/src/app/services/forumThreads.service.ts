import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ForumCategory} from "../models/forumCategory.model";
import {ForumThreadViewModel} from "../models/forumThreadView.model";
import {ForumThread} from "../models/forumThread/forumThread.model";
import {ForumCategoryDetailsModel} from "../models/forumCategoryDetails.model";
@Injectable({
  providedIn: 'root'
})
export class ForumThreadsService {

  constructor(private http: HttpClient) { }
  ForumThreadsOfCategory(id:number){
    return this.http.get<ForumThread[]>("api/ForumThread/" + id)
  }
  GetCategoryDetails(id:number){
    return this.http.get<ForumCategoryDetailsModel>("api/ForumThread/CategoryDetails/" + id)
  }
}
