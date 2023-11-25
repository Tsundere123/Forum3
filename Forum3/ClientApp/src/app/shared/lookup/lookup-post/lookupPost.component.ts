import {Component, Input} from '@angular/core';
import {LookupPost} from "../../../models/lookup/lookupPost.model";

@Component({
  selector: 'app-lookup-post',
  templateUrl: './lookupPost.component.html',
})
export class LookupPostComponent {
  @Input() post: LookupPost;
}
