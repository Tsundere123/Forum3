import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Search} from "../models/search.model";
import {LookupThread} from "../models/lookup/lookupThread.model";
import {LookupPost} from "../models/lookup/lookupPost.model";
import {LookupMember} from "../models/lookup/lookupMember.model";

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private http: HttpClient) { }

  searchIndex(query: string) {
    return this.http.get<Search>('api/Search?query=' + query);
  }

  searchThreads(query: string) {
    return this.http.get<LookupThread[]>('api/Search/threads?query=' + query);
  }

  searchPosts(query: string) {
    return this.http.get<LookupPost[]>('api/Search/posts?query=' + query);
  }

  searchMembers(query: string) {
    return this.http.get<LookupMember[]>('api/Search/members?query=' + query);
  }
}
