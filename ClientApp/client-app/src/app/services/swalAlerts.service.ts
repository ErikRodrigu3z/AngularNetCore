import { Injectable } from '@angular/core';
import { sweetPosition, sweetBackground, sweetColor, sweetIcon } from 'src/app/enums/alert/sweetEnums';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SwalAlertsService {

  constructor() { }

  async ShowToast(message: string, position: sweetPosition, type: sweetBackground):Promise<void>{
    
    Swal.fire({              
      position: position,        
      text: message,
      showConfirmButton: false,
      toast: true,
      color: sweetColor.white,
      background: type,
      timer: 5000
    });
    //Swal.close();
  }

  async ShowLoader():Promise<void>{   
    Swal.fire({      
      text: 'Loading .....',
      allowOutsideClick: false,      
      background: sweetColor.trasparent,
      color: sweetColor.white,
      didOpen: () => {
        Swal.showLoading()
      }      
    });
  }

  async CloseLoader():Promise<void>{ 
      Swal.close();
  }

}
