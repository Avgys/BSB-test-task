import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

  constructor(
      private formBuilder: FormBuilder,
      private accountService: AccountService,
      private router: Router
  ) { 
      
      this.form = this.formBuilder.group({
          login: ['', Validators.required],
          password: ['', Validators.required]
      });
  }

  ngOnInit() {
      this.form = this.formBuilder.group({
          login: ['', Validators.required],
          password: ['', Validators.required]
      });
  }

  onSubmit() {
      this.submitted = true;

      if (this.form.invalid) {
          return;
      }

      this.loading = true;
      this.accountService.login(this.form.controls.login.value, this.form.controls.password.value)          
        .subscribe(                  
          data => {                  
              this.router.navigate(["/"]);                    
          }
      )
  }
}
