import {Component, Input} from '@angular/core';
import {LookupThread} from "../../../models/lookup/lookupThread.model";

@Component({
  selector: 'app-lookup-thread',
  templateUrl: './lookup-thread.component.html',
})
export class LookupThreadComponent {
  @Input() thread: LookupThread;
}
