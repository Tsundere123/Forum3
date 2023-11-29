import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { LookupMember } from "../models/lookup/lookup-member.model";

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private http: HttpClient) { }

  getData() {
    return this.http.get<LookupMember[]>('api/Member/');
  }
}
