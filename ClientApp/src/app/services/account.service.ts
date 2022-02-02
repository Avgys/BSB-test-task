import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { Account } from '../models/Account';

@Injectable({
  providedIn: 'root'
})
export class AccountService {  
  currentApi = "https://185.152.139.93:443/api/accounts";
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  isLogged: boolean = false;
  user: Account | null = null;
  listeners: ((data: any) => any)[] = []
  constructor(private http: HttpClient) {
  }

  listen(listener: ((data: any) => any)){
    this.listeners.push(listener);
  }

  getAccountInfo(){
    let url = `${this.currentApi}/`;       
    this.http.get<Account>(url, this.httpOptions).subscribe(data => {
      this.user = data;
      this.isLogged = true;
      this.listeners.forEach(element => {
        element(this.isLogged)
      });
      console.log(this.user);
    }); 
  }

  login(login: string, password: string) {   
    let url = `${this.currentApi}/` + `login`; 
    let account = { "login" : login, "password" : password } as Account;
    console.log(account);
    let result = this.http.post<Account>(url, account, this.httpOptions);
    result.subscribe(data => {
      this.user = data;
      this.isLogged = true;
    });

    return result;
  }

  logout() { 
      let url = `${this.currentApi}/` + 'logout';        
      return this.http.post(url, null, this.httpOptions).subscribe(data => 
        {
          this.isLogged = false;
          this.user = null;
          this.listeners.forEach(element => {
            element(this.isLogged)
          });
        });
  }
}
