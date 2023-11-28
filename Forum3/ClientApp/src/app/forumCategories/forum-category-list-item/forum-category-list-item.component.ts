import {Component, Input} from '@angular/core';
import {ForumCategory} from "../../models/forumCategory.model";

@Component({
  selector: 'app-forum-category-list-item',
  templateUrl: './forum-category-list-item.component.html'
})
export class ForumCategoryListItemComponent {
  @Input() currentCategory: ForumCategory;
}
