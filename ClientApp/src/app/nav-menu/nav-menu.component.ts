import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  isLogged = false;
  constructor(
    private http: AccountService,    
    private router: Router
    ) {
    this.checkLog();    
  }

  collapse() {
    this.isExpanded = false;
  }

  checkLog(){
    let func = (data) => {
      this.isLogged = data
    };
    this.http.listen(func);
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout(){
    this.http.logout();
    // window.location.reload();
  }
}
