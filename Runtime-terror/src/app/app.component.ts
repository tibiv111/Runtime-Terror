import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { UserInterface } from '../models/users/UsersInterface'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public users!: UserInterface;

  constructor(http: HttpClient, private usersService: UsersService) {  }
  ngOnInit(): void {
    this.usersService.getAllUsersData().subscribe((data) => {
      this.users = data;
})
    }

  title = 'Runtime-terror';
}

