import { Component, OnInit } from '@angular/core';
/* import { UserService } from "src/services/user.service"; */
import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { NgxSpinnerService } from 'ngx-spinner';
import { CommonService } from '../../../services/common.service';
/* import { BroadcastMessageServiceService } from 'src/services/broadcast-message-service.service';
import { CommonService } from 'src/services/common.services'; */

@Component({
  selector: 'app-information-board',
  templateUrl: './information-board-show.component.html',
  styleUrls: ['./information-board-show.component.css']
})
export class InformationBoardShowComponent implements OnInit {

  messageList = [{"title":"John", "lastName":"Smith"}, {"title":"John", "lastName":"Smith"}, {"title":"John", "lastName":"Smith"}, {"title":"John", "lastName":"Smith"}];
  userList = [];
  pageNumber: number = 1;
  constructor(
   /*  public commonService: CommonService,
    private broadcastService: BroadcastMessageServiceService, */
    public translate: TranslateService,
    private route: Router,
    private SpinnerService: NgxSpinnerService,
    private commonService: CommonService
  ) {}

  ngOnInit() {
    /* if (this.commonService.roleMatch(["Shacho"]) && this.commonService.userData.firstTimeLogIn === true) {
      // FIRST TIME LOG IN IS FOR THE FIRST TIME REDIRECTION
      this.commonService.userData.firstTimeLogIn = false;
      //for redirection in the user role "show message"
      this.route.navigate(["/report/show"]);
    }
    this.getRecentBroadcastMessage(); */

    this.SpinnerService.show();

    this.commonService.fetchData().subscribe(
      data => {
        Object.entries(data).map(res => {
          this.userList.push(res[1]);
        });
        //after fetching data, spinner will hide
        this.SpinnerService.hide();
        // data.foreach(element => {
        //   this.reportList.push({
        //     firstName : element.FirstName,
        //     lastName: element.lastName
        //   })
        // })
        //this.reportList = JSON.parse(JSON.stringify(data));
        //this.reportList = data.value;
      },
      err => {}
    );
  
    console.log(this.userList);
  }

 /*  getRecentBroadcastMessage() {
    //before fetching data, spinner effect shows
    this.SpinnerService.show();
    this.broadcastService.getRecentBroadcastMessage().subscribe(data => {
      Object.entries(data).map(res => {
        this.messageList.push(res[1]);
      });
      //after fetching data, spinner will hide
      this.SpinnerService.hide();
      console.log(this.messageList);
    })
  } */

  
}
