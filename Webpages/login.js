//Here Data Fetch from database
function checkUser() {
  let user = document.getElementById("us").value;
  let password = document.getElementById("pas").value;
  var request = {
    method: "POST",
    // redirect:'follow',
    body: JSON.stringify({
      username: user,
      password: password,
    }),
    // Adding headers to the request
    headers: {
      "Content-type": "application/json; charset=UTF-8",
    },
  };
  fetch("http://localhost:64409/api/Login", request)
    .then((res) => {
      return res.json();
    })
    .then((data) => showstorage(data))
    .catch((error) => console.log(error));
}

// User data is stored in session storage
function showstorage(data) {
  if (data != null && data != undefined && data != "") {
    console.log(data);
    sessionStorage.setItem("token", data.token);
    sessionStorage.setItem("userid", data.userid);
    sessionStorage.setItem("username", data.username);
    sessionStorage.setItem("fId", data.CreatedBy);
  }
  loc();
}

// After token generation locate to next page
function loc() {
  if (sessionStorage.getItem("token") != null) {
    // alert("Succesful");
    window.location.href = "docs.html";
  } else {
    alert("Login Credentials are wrong");
  }
}
