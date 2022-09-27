import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
//properties

  toggleButton(){
    /* ========= sidebar toggle ======== */
    const sidebarNavWrapper = document.querySelector(".sidebar-nav-wrapper");
    const mainWrapper = document.querySelector(".main-wrapper");
    const menuToggleButton = document.querySelector("#menu-toggle");
    const menuToggleButtonIcon = document.querySelector("#menu-toggle i");
    const overlay = document.querySelector(".overlay");

    menuToggleButton?.addEventListener("click", () => {
      sidebarNavWrapper?.classList.toggle("active");
      overlay?.classList.add("active");
      mainWrapper?.classList.toggle("active");

          

      if (document.body.clientWidth > 1200) {
        if (menuToggleButtonIcon?.classList.contains("lni-chevron-left")) {
          menuToggleButtonIcon.classList.remove("lni-chevron-left");
          menuToggleButtonIcon.classList.add("lni-menu");
          sessionStorage.setItem('toggleButton', 'true');
        } else {
          menuToggleButtonIcon?.classList.remove("lni-menu");
          menuToggleButtonIcon?.classList.add("lni-chevron-left");
          sessionStorage.setItem('toggleButton', 'false');

        }
      } else {
        if (menuToggleButtonIcon?.classList.contains("lni-chevron-left")) {
          menuToggleButtonIcon.classList.remove("lni-chevron-left");
          menuToggleButtonIcon.classList.add("lni-menu");
        }
      }
  });

  overlay?.addEventListener("click", () => {
    sidebarNavWrapper?.classList.remove("active");
    overlay.classList.remove("active");
    mainWrapper?.classList.remove("active");
  });

  }

  constructor() { }

  ngOnInit(): void {
  }

}
