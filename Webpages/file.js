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

searchBtn.addEventListener("click", () => {
  sidebar.classList.remove("close");
});

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
// Upload Files in Folder
async function createFiles() {
  try {
    var file = document.getElementById("input1").files[0];
    var abc = new Date();
    var formData = new FormData();
    formData.append("file", file);
    var requestOptions = {
      method: "POST",
      body: formData,
    };
    await fetch(
      "http://localhost:64409/api/Documents/upload/" +
        sessionStorage.getItem("userid") +
        "/" +
        abc.toISOString() +
        "/" +
        sessionStorage.getItem("fId"),
      requestOptions
    ).then((fileCreateResponse) => {
      console.log(fileCreateResponse);

      listFiles();
      modal.classList.toggle("modal-open");
    });
  } catch (err) {
    console.log(err);
  }
}
// list of files dispaly
function listFiles() {
  try {
    var create = document.getElementById("create");
    create.innerHTML = "";
    fetch(
      `${constants.apiBasePath}Documents/` + sessionStorage.getItem("userid"),
      {
        method: "GET",
      }
    )
      .then((response) => response.json())
      .then((folders) => {
        console.log(folders);
        folders.forEach((documents) => {
          var create = document.getElementById("create");
          var art = document.createElement("article");
          art.setAttribute("id", "section");
          const Dname = documents.dName;
          const Did = documents.dId;
          let doc = "";
          doc += `<i  class='bx bxs-folder-open bx-lg' style='color:rgba(23,159,226,0.78);cursor:pointer;'> </i>`;
          doc += `<button id="filebtn" style="text-decoration: none;border: 0px;cursor:pointer;background:white;width:auto;height:50px;margin-left:5px;margin-top:30px;font-weight:500;font-size:25px;color:#707070;"> ${Dname} </button>`;
          doc += `<i class='bx bx-trash bx-sm' onclick="caution(${Did})" style="cursor:pointer; float:right;margin-top:70px;margin-right:0px;color:rgba(0,0,0,0.38)"></i>`;
          doc += `<i class='bx bx-star bx-sm' onclick="starredfolder(${Did})"style="cursor:pointer;float:right;margin-right:3px;margin-top:70px;color:rgba(0,0,0,0.46)"></i>`;
          doc += `<i class='bx bx-info-circle bx-sm' onclick='opendetails(${documents.dId},"${documents.dName}","${documents.createdBy}","${documents.createdAt}" ) ' style="float:right;cursor:pointer;margin-top:70px;margin-right:5px;color:rgba(0,0,0,0.46)"></i>`;
          art.innerHTML = doc;
          create.appendChild(art);
        });
      });
  } catch (err) {
    console.log(err);
  }
}

function onLoad() {
  listFiles();
  document.getElementById("adminName").innerHTML =
    "Hi, " + sessionStorage.getItem("username") + "!";
}
onLoad();
//  file path
const next = document.getElementById("hello");
// logout from file page
function logout() {
  sessionStorage.clear();
  window.location.href = "index.html";
}
//searching files
function searchItem() {
  try {
    var search = document.getElementById("search");

    // console.log(search);

    var create = document.getElementById("create");

    create.innerHTML = "";

    fetch(
      "http://localhost:64409/api/Documents/" +
        sessionStorage.getItem("userid") +
        "/" +
        search
    )
      .then((response) => response.json())

      .then((folders) => {
        //console.log(folders);

        folders.forEach((documents) => {
          var create = document.getElementById("create");
          var art = document.createElement("article");
          art.setAttribute("id", "section");
          const Dname = documents.dName;
          const Did = documents.dId;

          // fold.style.backgroundColor = "red";
          // console.log(fname);
          //console.log(fId);
          art.innerHTML = `<button id="filebtn" onclick ="createFiles(${Did})"> ${Dname} </button>`;
          create.appendChild(art);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
//Sweetalert applied on onclicking trash icon
function caution(Did) {
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
          deletefile(Did)
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

//delete folder
function deletefile(Did) {
  var raw = "";
  var requestOptions = {
    method: "DELETE",
    body: raw,
    redirect: "follow",
  };

  var deleteurl = "http://localhost:64409/api/Documents/" + Did;
  fetch(deleteurl, requestOptions)
    .then((response) => response.text())
    .then((result) => console.log(listFiles()))
    .catch((error) => console.log("error", error));
  // location.reload();
}
//viewdetails
function opendetails(dId, dName, createdBy, createdAt) {
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
