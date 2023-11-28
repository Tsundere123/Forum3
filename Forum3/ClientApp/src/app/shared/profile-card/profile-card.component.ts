import { Component, Input } from '@angular/core';
import { LookupMember } from "../../models/lookup/lookupMember.model";

@Component({
  selector: 'app-profile-card',
  templateUrl: './profile-card.component.html'
})
export class ProfileCardComponent {
  @Input() member: LookupMember;
}
