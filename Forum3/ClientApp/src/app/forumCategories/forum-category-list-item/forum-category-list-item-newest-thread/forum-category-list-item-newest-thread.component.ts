import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {LookupThread} from "../../../models/lookup/lookupThread.model";

@Component({
  selector: 'app-forum-category-list-item-newest-thread',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './forum-category-list-item-newest-thread.component.html',
  styleUrl: './forum-category-list-item-newest-thread.component.css'
})
export class ForumCategoryListItemNewestThreadComponent {
  @Input() latestThread: LookupThread;


}
