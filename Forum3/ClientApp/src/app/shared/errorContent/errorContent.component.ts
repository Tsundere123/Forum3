import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-error-content',
  templateUrl: './errorContent.component.html',
})
export class ErrorContentComponent {
  @Input() isError: boolean = true;
}
