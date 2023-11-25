import {Component, Input} from '@angular/core';
import {LookupMember} from "../../../models/lookup/lookupMember.model";

@Component({
  selector: 'app-lookup-member',
  templateUrl: './lookupMember.component.html',
})
export class LookupMemberComponent {
  @Input() member: LookupMember;
}
