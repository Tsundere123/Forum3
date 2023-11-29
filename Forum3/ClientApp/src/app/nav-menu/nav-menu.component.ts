import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  searchForm: FormGroup;
  isExpanded = false;

  constructor(private formBuilder: FormBuilder, private router: Router) {
    this.searchForm = formBuilder.group({
      searchQuery: ['', Validators.required]
    });
  }

  onSubmitSearch() {
    let query = this.searchForm.value.searchQuery;
    this.searchForm.patchValue({
      searchQuery: ''
    });
    this.router.navigate(["/search", query]);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
