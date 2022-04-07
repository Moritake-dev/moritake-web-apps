import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageCreateModel } from 'src/models/message';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';

import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { CommonService } from '../../../services/common.service';
import { AuthService } from "../../../services/auth.service";

@Component({
  selector: 'app-information-board-create',
  templateUrl: './information-board-create.component.html',
  styleUrls: ['./information-board-create.component.css']
})
export class InformationBoardCreateComponent implements OnInit {

  public Editor = ClassicEditor;

  messageCreateForm: FormGroup;
  submitted = false;
  userDataSubscription;
  userData;
  messageFormData: any = {};

  // ngx-modal configuration and implementation
  modalRef: BsModalRef;
  modalConfig = {
    backdrop: true,
    class: "modal-xl"
  };
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private modalService: BsModalService,
    private commonService: CommonService,
    private authService: AuthService
  ) {
    // getting user data

    this.commonService.getUserDetails();
    this.userDataSubscription = this.commonService.userData
      .asObservable()
      .subscribe(data => {
        this.userData = data;
        console.log(this.userData);
      });

      ClassicEditor.defaultConfig = {
        toolbar: {
          items: [
            'heading',
            '|',
            'bold',
            'italic',
            '|',
            'bulletedList',
            '|',
            'undo',
            'redo',
            'fontColor',
            'underline'
          ]
        },
      };

   }

  ngOnInit() {

    this.messageCreateForm = this.formBuilder.group({
      messageTitle: ["", Validators.required],
      messageDetail: ["", Validators.required],
    });
  }

  get messageCreateFormControl() {
    return this.messageCreateForm.controls;
  }

  onSubmit(template: TemplateRef<any>) {
    this.submitted = true;
    if (this.messageCreateForm.invalid) {
      return;
    }

    this.messageFormData.messageTitle = this.messageCreateForm.controls["messageTitle"].value;
    this.messageFormData.messageDetail = this.messageCreateForm.controls["messageDetail"].value;



    this.modalRef = this.modalService.show(template, this.modalConfig);
    //console.log(this.messageFormData);
    
  }

  modalConfirm() {
    // calling api to save data

    let messageModel = new MessageCreateModel();

    messageModel.userId = this.userData.userId;
    messageModel.messageTitle = this.messageFormData.messageTitle;
    messageModel.messageDetail = this.messageFormData.messageDetail;
    
    this.commonService.saveMessage(messageModel).subscribe(
      data => {
        console.log(data);
        this.router.navigateByUrl("/", { skipLocationChange: true }).then(() => {
          this.router.navigate(["/home"]);
        });
      },
      error => {
        console.log(error);
      }
    );
    
    this.modalRef.hide();

  }

  modalDecline() {
    this.submitted = false;
    if (this.messageCreateForm.invalid) {
      return;
    }

    this.modalRef.hide();
  }

}
