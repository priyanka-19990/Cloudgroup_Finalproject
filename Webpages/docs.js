const body = document.querySelector("body"),
  sidebar = body.querySelector("nav"),
  toggle = body.querySelector(".toggle"),
  searchBtn = body.querySelector(".search-box"),
  modeSwitch = body.querySelector(".toggle-switch"),
  modeText = body.querySelector(".mode-text");

toggle.addEventListener("click", () => {
  sidebar.classList.toggle("close");
  const isClose=sidebar.classList.contains("close")
  const classBody = document.querySelector('.class')
  if(isClose){
    classBody.style.marginLeft='100px'
  }else{
    classBody.style.marginLeft='300px'
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
// console.log(form);
var curr = new Date();
function createFolders() {
  try {
    fetch(`${constants.apiBasePath}Folders`, {
      body: JSON.stringify({
        fName: form.value,
        createdBy: sessionStorage.getItem("userid"),
        createdAt: curr.toISOString(),
        isDeleted: 0
      }),
      method: "POST",
      // redirect: "follow",
      headers: {
        "Content-Type": "application/json",
      },
    }).then((folderCreateResponse) => {
      console.log(folderCreateResponse);
     listFolders();
     modal.classList.toggle("modal-open");
    });
  } catch (err) {
    console.log(err);
  }
}

function listFolders() {
  try {
    var create = document.getElementById("create");
    create.innerHTML = '';
    fetch(`${constants.apiBasePath}Folders/` + sessionStorage.getItem("userid"), {
        method: 'GET',
      })
      .then((response) => response.json())
      .then((folders) => {
        // console.log(folders);
        folders.forEach((folder) => {
          var create = document.getElementById("create");
          var art = document.createElement("article");
          art.setAttribute("id", "section");
          const fname = folder.fName;
          const folderid = folder.fId;
  
          let doc='';
          doc+=  `<i class='bx bx-folder-open bx-md' style="cursor:pointer;"></i>`;
          doc+=  `<button id="filebtn" onclick ="createfiles(${folderid})"   style="text-decoration: none;border: 0px;cursor:pointer;background:white;margin-left:5px;margin-top:5px;font-weight:400;font-size:25px"> ${fname} </button>`;
          doc+=  `<i class='bx bxs-trash bx-sm' onclick="caution(${folderid})" style="cursor:pointer; float:right;"></i>`;
          doc+=  `<i class='bx bx-info-circle bx-sm' onclick='opendetails(${folder.fId},"${folder.fName}","${folder.createdBy}","${folder.createdAt}" ) ' style="float:right;cursor:pointer;margin-top:70px;margin-right:-20px;"></i>`
          art.innerHTML=doc;
          create.appendChild(art);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
function createfiles(fId) {
  sessionStorage.setItem("fId", fId);
  window.location.href = "file.html";
}
function onLoad() {
  listFolders();
  document.getElementById("adminName").innerHTML =
    "Hi, " + sessionStorage.getItem("username") + "!";
}
onLoad();
//  file path
const next = document.getElementById('hello');
function logout() {
  sessionStorage.clear();
  window.location.href = "index.html";
}
function searchItem() {
  try {
    var search = document.getElementById("searchButton").value;

    // console.log(search);

    var create = document.getElementById("create");

    create.innerHTML = "";

    fetch(`http://localhost:64409/api/Folders/${sessionStorage.getItem("userid")}/${search}`,
    {
      method:"GET"
    }
    )
      .then((response) => response.json())

      .then((folders) => {
        //console.log(folders);

        folders.forEach((folder) => {
          var create = document.getElementById("create");
          var art = document.createElement("article");
          art.setAttribute("id", "section");
          const fname = folder.fName;
          const folderid = folder.fId;
          let doc='';
          doc+=  `<i class='bx bx-folder-open bx-md' style="cursor:pointer;"></i>`;
          doc+=  `<button id="filebtn" onclick ="createfiles(${folderid})"   style="text-decoration: none;border: 0px;cursor:pointer;background:white;margin-left:5px;margin-top:5px;font-weight:400;font-size:25px"> ${fname} </button>`;
          doc+=  `<i class='bx bxs-trash bx-sm' onclick="deletefolder(${folderid})" style="cursor:pointer; float:right;"></i>`;
          doc+=  `<i class='bx bx-info-circle bx-sm' onclick='opendetails(${folder.fId},"${folder.fName}","${folder.createdBy}","${folder.createdAt}" ) ' style="float:right;cursor:pointer;margin-top:70px;margin-right:-20px;"></i>`
          art.innerHTML=doc;
          create.appendChild(art);
        });
      });
  } catch (err) {
    console.log(err);
  }
}

function caution(folderId){
  if(confirm("Sure you want to delete it?")){
    deletefolder(folderId)
  }
}

//delete folder
function deletefolder(folder) {
  var raw = "";
var requestOptions = {
  method: 'DELETE',
  body: raw,
  redirect: 'follow'
};

var deleteurl = "http://localhost:64409/api/Folders/" + folder;
fetch(deleteurl,requestOptions)
.then(response=>response.text())
.then(result => console.log(listFolders()))
  .catch(error => console.log('error', error));
  // location.reload();  
}
//viewdetails
function opendetails(folderId,foldername,createdBy,createdAt)
{
  alert(
  "FolderId : " +folderId+ "\n"+
  "Folder Name :" +foldername + "\n"+
  "Created By : " +createdBy + "\n"+
  "Created At : " +createdAt+ "\n"
  );

}

