import { Component, OnInit } from '@angular/core';
/* import { AuthenticationService } from 'src/services/authentication.service';
import { User } from 'src/models/user'; */
import { Router } from '@angular/router';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  login() {
    this.authService.login();
  }

}
