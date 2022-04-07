import { Component } from '@angular/core';
import { logging } from 'protractor';
import { TranslateService } from "@ngx-translate/core";
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'mg-solutions';

  constructor(private authService: AuthService,
    translate: TranslateService) {
      //setting the default language
    translate.setDefaultLang("jp");

    // DETECTING THE BROWSER LANGUAGE AND SET ACCORDINGLY
    let userLanguage;
    userLanguage = navigator.language;

    if (userLanguage === "en-US") {
      userLanguage = "en";
    }
    if (userLanguage === "ja") {
      userLanguage = "jp";
    }
    translate.use(userLanguage);
  }

  login() {
    this.authService.login();
  }
}
