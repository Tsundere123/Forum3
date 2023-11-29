import {Component, Input} from '@angular/core';
import {LookupPost} from "../../../models/lookup/lookup-post.model";

@Component({
  selector: 'app-lookup-post',
  templateUrl: './lookup-post.component.html',
})
export class LookupPostComponent {
  @Input() post: LookupPost;
}
