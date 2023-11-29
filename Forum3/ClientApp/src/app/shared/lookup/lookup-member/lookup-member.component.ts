import {Component, Input} from '@angular/core';
import {LookupMember} from "../../../models/lookup/lookup-member.model";

@Component({
  selector: 'app-lookup-member',
  templateUrl: './lookup-member.component.html',
})
export class LookupMemberComponent {
  @Input() member: LookupMember;
}
