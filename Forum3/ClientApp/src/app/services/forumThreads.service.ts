import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ForumCategory} from "../models/forumCategory.model";
import {ForumThreadViewModel} from "../models/forumThreadView.model";
import {ForumThread} from "../models/forumThread/forumThread.model";
import {ForumCategoryDetailsModel} from "../models/forumCategoryDetails.model";
import {ForumThreadDetailsModel} from "../models/forumThreadDetails.model";
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
  GetThreadDetails(threadId:number){
    return this.http.get<ForumThreadDetailsModel>("api/ForumThread/ThreadDetails/" + threadId)
  }

  CreateThread(categoryId: number, newThread: any){
    return this.http.post<any>("api/ForumThread/Create/" + categoryId, newThread);
  }

  EditCurrentThread(threadId: number, newTitle: any){
    return this.http.post<any>("api/ForumThread/EditThread/" + threadId, newTitle);
  }

  PermaDeleteCurrentThread(threadId: number){
    return this.http.delete<any>("api/ForumThread/PermaDelete/" + threadId);
  }

  SoftDeleteCurrentThread(threadId: number){
    return this.http.delete<any>("api/ForumThread/SoftDelete/" + threadId);
  }

  UnSoftDeleteCurrentThread(threadId: number){
    return this.http.delete<any>("api/ForumThread/UnSoftDelete/" + threadId);
  }

  pinCurrentThread(threadId: number){
    return this.http.get<any>("api/ForumThread/PinThread/" + threadId);
  }

  unpinCurrentThread(threadId: number){
    return this.http.get<any>("api/ForumThread/UnPinThread/" + threadId);
  }

  lockCurrentThread(threadId: number){
    return this.http.get<any>("api/ForumThread/LockThread/" + threadId);
  }

  unlockCurrentThread(threadId: number){
    return this.http.get<any>("api/ForumThread/UnLockThread/" + threadId);
  }
}
