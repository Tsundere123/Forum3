import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-loading-content',
  templateUrl: './loadingContent.component.html',
})
export class LoadingContentComponent {
  @Input() isLoading: boolean = true;
}
