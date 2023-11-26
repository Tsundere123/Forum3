import {
  Component,
  OnInit,
} from '@angular/core';
import {LookupMember} from "../models/lookup/lookupMember.model";
import {MemberService} from "../services/member.service";

@Component({
  selector: 'app-member-list',
  templateUrl: './memberList.component.html'
})
export class MemberListComponent implements OnInit {
  members: LookupMember[] = [];

  constructor(private memberService: MemberService) { }

  ngOnInit() {
    this.memberService.getData().subscribe({
      next:(data) => {
        this.members = data;
      },
      error:(response) =>{
        console.log(response);
      }
    })
  }
}
