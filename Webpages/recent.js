const body = document.querySelector("body"),
  sidebar = body.querySelector("nav"),
  toggle = body.querySelector(".toggle"),
  searchBtn = body.querySelector(".search-box"),
  modeSwitch = body.querySelector(".toggle-switch"),
  modeText = body.querySelector(".mode-text");

toggle.addEventListener("click", () => {
  sidebar.classList.toggle("close");
  const isClose = sidebar.classList.contains("close");
  const classBody = document.querySelector(".class");
  if (isClose) {
    classBody.style.marginLeft = "100px";
  } else {
    classBody.style.marginLeft = "300px";
  }
});
// search button functonality
searchBtn.addEventListener("click", () => {
  sidebar.classList.remove("close");
});
// change mode light to dark dark to light
modeSwitch.addEventListener("click", () => {
  body.classList.toggle("dark");
  if (body.classList.contains("dark")) {
    modeText.innerText = "Light mode";
  } else {
    modeText.innerText = "Dark mode";
  }
});
const modal = document.querySelector("#add-new-folder-modal");
const newFolderBtn = document.querySelector("#new-folder-btn");
newFolderBtn.addEventListener("click", (e) => {
  modal.classList.toggle("modal-open");
});
const closeModalButton = document.querySelectorAll(".modal-close");
closeModalButton.forEach((item) => {
  item.addEventListener("click", (e) => {
    modal.classList.toggle("modal-open");
  });
});

const constants = {
  apiBasePath: "http://localhost:64409/api/",
};

const form = document.getElementById("input1");
var curr = new Date();
// list of recent folder
function listFolders() {
  try {
    var create = document.getElementById("create");
    var date = new Date();
    create.innerHTML = "";
    fetch(
      "http://localhost:64409/api/Folders/Recent/" +
        sessionStorage.getItem("userid") +
        "/" +
        date.getHours(),
      {
        method: "GET",
      }
    )
      .then((response) => response.json())
      .then((folders) => {
        debugger;
        // console.log(folders);
        folders.forEach((folder) => {
          var create = document.getElementById("create");
          var art = document.createElement("article");
          art.setAttribute("id", "section");
          const fname = folder.fName;
          const folderid = folder.fId;

          let doc = "";
          doc += `<i  class='bx bxs-folder-open bx-lg' style='color:rgba(23,159,226,0.78);cursor:pointer;'> </i>`;
          doc += `<button id="filebtn" onclick ="createfiles(${folderid})" style="text-decoration: none;border: 0px;cursor:pointer;background:white;width:auto;height:50px;margin-left:5px;margin-top:30px;font-weight:500;font-size:25px;color:#707070;"> ${fname}</button>`;
          doc += `<i class='bx bx-trash bx-sm' onclick="caution(${folderid})" style="cursor:pointer; float:right;margin-top:70px;margin-right:0px;color:rgba(0,0,0,0.38)"></i>`;
          doc += `<i class='bx bx-star bx-sm' onclick="starredfolder(${folderid})"style="cursor:pointer;float:right;margin-right:3px;margin-top:70px;color:rgba(0,0,0,0.46)"></i>`;
          doc += `<i class='bx bx-info-circle bx-sm' onclick='opendetails(${folder.fId},"${folder.fName}","${folder.createdBy}","${folder.createdAt}" ) ' style="float:right;cursor:pointer;margin-top:70px;margin-right:5px;color:rgba(0,0,0,0.46)"></i>`;

          art.innerHTML = doc;
          create.appendChild(art);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
// loacate to file page
function createfiles(fId) {
  sessionStorage.setItem("fId", fId);
  window.location.href = "file.html";
}
// Load the page with user name
function onLoad() {
  listFolders();
  document.getElementById("adminName").innerHTML =
    "Hi, " + sessionStorage.getItem("username") + "!";
}
onLoad();
//  file path
const next = document.getElementById("hello");
function logout() {
  sessionStorage.clear();
  window.location.href = "index.html";
}
// Search folders
function searchItem() {
  try {
    var search = document.getElementById("searchButton").value;
    var create = document.getElementById("create");
    create.innerHTML = "";
    fetch(
      `http://localhost:64409/api/Folders/${sessionStorage.getItem(
        "userid"
      )}/${search}`,
      {
        method: "GET",
      }
    )
      .then((response) => response.json())
      .then((folders) => {
        folders.forEach((folder) => {
          var create = document.getElementById("create");
          var art = document.createElement("article");
          art.setAttribute("id", "section");
          const fname = folder.fName;
          const folderid = folder.fId;
          let doc = "";
          doc += `<i class='bx bx-folder-open bx-md' style="cursor:pointer;"></i>`;
          doc += `<button id="filebtn" onclick ="createfiles(${folderid})"   style="text-decoration: none;border: 0px;cursor:pointer;background:white;margin-left:5px;margin-top:5px;font-weight:400;font-size:25px"> ${fname} </button>`;
          doc += `<i class='bx bxs-trash bx-sm' onclick="deletefolder(${folderid})" style="cursor:pointer; float:right;"></i>`;
          doc += `<i class='bx bx-info-circle bx-sm' onclick='opendetails(${folder.fId},"${folder.fName}","${folder.createdBy}","${folder.createdAt}" ) ' style="float:right;cursor:pointer;margin-top:70px;margin-right:-20px;"></i>`;
          art.innerHTML = doc;
          create.appendChild(art);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
// sweetalert on delete
function caution(folderId) {
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: "btn btn-success",
      cancelButton: "btn btn-danger",
    },
    buttonsStyling: false,
  });
  swalWithBootstrapButtons
    .fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,

      cancelButtonText: " No, cancel!",
      confirmButtonText: " Yes,  delete it!",

      reverseButtons: true,
    })
    .then((result) => {
      if (result.isConfirmed) {
        swalWithBootstrapButtons.fire(
          "Deleted!",
          "Your file has been deleted.",
          "success",
          deletefolder(folderId)
        );
      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire(
          "Cancelled",
          "Your imaginary file is safe :)",
          "error"
        );
      }
    });
}

//starred
function starredfolder(folder) {
  var raw = "";
  var requestOptions = {
    method: "PUT",
    body: raw,
    redirect: "follow",
  };
  fetch("http://localhost:64409/api/Folders/Starred/" + folder, requestOptions)
    .then((response) => response.text())
    .then((result) => {
      console.log(result);
      listFolders();
    })
    .catch((error) => console.log("error", error));
}
//soft delete
function deletefolder(folder) {
  var raw = "";
  var requestOptions = {
    method: "PUT",
    body: raw,
    redirect: "follow",
  };
  fetch(
    "http://localhost:64409/api/Folders/SoftDelete/" + folder,
    requestOptions
  )
    .then((response) => response.text())
    .then((result) => {
      console.log(result);
      listFolders();
    })
    .catch((error) => console.log("error", error));
}

//viewdetails
function opendetails(folderId, foldername, createdBy, createdAt) {
  Swal.fire({
    title:
      "FolderId : " +
      folderId +
      "\n" +
      "\n" +
      "Folder Name :" +
      foldername +
      "\n" +
      "\n" +
      "Created By : " +
      createdBy +
      "\n" +
      "\n" +
      "Created At : " +
      "\n" +
      createdAt +
      "\n" +
      "\n",
    showClass: {
      popup: "animate__animated animate__fadeInDown",
    },
    hideClass: {
      popup: "animate__animated animate__fadeOutUp",
    },
  });
}
