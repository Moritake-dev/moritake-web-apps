import { Component, OnInit } from "@angular/core";
/* import { User } from "src/models/user";
import { AuthenticationService } from "src/services/authentication.service";
import { UserRole } from "src/models/roles"; */
import { Location } from "@angular/common";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthService } from "src/services/auth.service";
/* import { UserService } from "src/services/user.service";
import { CommonService } from "src/services/common.services"; */


@Component({
  selector: "app-top-bar",
  templateUrl: "./top-bar.component.html",
  styleUrls: ["./top-bar.component.css"],
})
export class TopBarComponent implements OnInit {
  /* userDataSubscription: any;
  userData = new User();
  userName = "";
  userAdditionalInfo;
  route: string;
  urlAppSelect; */
  constructor(
   /*  private authService: AuthenticationService,
    public userService: UserService,
    public commonService: CommonService, */
    location: Location,
    router: Router,
    route: ActivatedRoute,
    private authService: AuthService
  ) {
    const segmentNames = location.path();
    
     /*  if (segmentNames.includes("information-board")) {
        this.urlAppSelect = true;
      } else {
        this.urlAppSelect = false;
      } */
    

    // router.events.subscribe((val) => {
    //   if(location.path() != ''){
    //     this.route = location.path();
    //     if(this.route == '/information-board'){
    //       this.urlAppSelect = true;
    //     }
    //     else {
    //       this.urlAppSelect = false;
    //     }
    //   } else {
    //     this.route = ''
    //   }
    // });

    /* this.userDataSubscription = this.authService.userData
      .asObservable()
      .subscribe((data) => {
        this.userData = data;
        this.userName = this.userData.fullName;
        console.log(this.userData);
      }); */
  }

  ngOnInit() {
    //this.getAdditionalUserInfo();
  }

  logout() {
    this.authService.signout();
  }

  // getAdditionalUserInfo() {
  //   this.userAdditionalInfo = this.authService.getUserAdditionalDetail(this.userData.userId).subscribe(
  //     data => {
  //       this.userAdditionalInfo.firstName = data.firstName;
  //       this.userAdditionalInfo.lastName = data.lastName;
  //       this.userAdditionalInfo.sex = data.sex;
  //       this.userAdditionalInfo.address = data.address;
  //     }
  //   );
  //   localStorage.setItem("userInfo", this.userAdditionalInfo);
  // }

  /* onLogout() {
    this.authService.logout();
  } */
}


