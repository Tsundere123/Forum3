import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ForumCategory} from "../models/forumCategory.model";
import {ForumThreadViewModel} from "../models/forumThreadView.model";
import {ForumThread} from "../models/forumThread/forumThread.model";
@Injectable({
  providedIn: 'root'
})
export class ForumThreadsService {

  constructor(private http: HttpClient) { }
  getData(id:number){
    return this.http.get<ForumThreadViewModel>("api/ForumThread/" + id)
  }
}
