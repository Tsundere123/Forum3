import {
  Component,
  OnInit,
} from '@angular/core';
import {LookupMember} from "../models/lookup/lookupMember.model";
import {MemberService} from "../services/member.service";

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html'
})
export class MemberListComponent implements OnInit {
  isLoading: boolean = true;
  isError: boolean = false;

  members: LookupMember[] = [];
  filteredMembers: LookupMember[] = [];

  constructor(private memberService: MemberService) { }

  private _filter: string = '';
  get filter(): string {
    return this._filter;
  }

  set filter(value: string) {
    this._filter = value;
    this.filteredMembers = this.filter ? this.performFilter(this.filter) : this.members;
  }

  performFilter(filterBy: string): LookupMember[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.members.filter((member: LookupMember) =>
      member.userName.toLocaleLowerCase().includes(filterBy));
  }

  ngOnInit() {
    this.memberService.getData().subscribe({
      next:(data) => {
        this.members = data;
        this.filteredMembers = this.members;

        this.isLoading = false;
      },
      error:(response) =>{
        console.log(response);
        this.isError = true;
        this.isLoading = false;
      }
    })
  }
}
