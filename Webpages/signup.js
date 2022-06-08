        const togglePassword = document.querySelector('#togglePassword');
        const password = document.querySelector('#password');
        togglePassword.addEventListener('click', function (e) {
            const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
            password.setAttribute('type', type);
            this.classList.toggle('bi-eye');
        });
     
        const togglePassword1 = document.querySelector('#togglePassword1');
        const password1 = document.querySelector('#password1');
        togglePassword1.addEventListener('click', function (e) {
            const type = password1.getAttribute('type') === 'password' ? 'text' : 'password';
            password1.setAttribute('type', type);
            this.classList.toggle('bi-eye');
        });


        function validate_password() {
            var password = document.getElementById('password').value;
            var password1 = document.getElementById('password1').value;
            if (password != password1) {
                document.getElementById('wrong_pass_alert').style.color = 'red';
                document.getElementById('wrong_pass_alert').innerHTML
                  = '      ☒ Use same password';
                document.getElementById('create').disabled = true;
                document.getElementById('create').style.opacity = (0.4);
            } else {
                document.getElementById('wrong_pass_alert').style.color = 'green';
                document.getElementById('wrong_pass_alert').innerHTML =
                    '      🗹 Password Matched';
                document.getElementById('create').disabled = false;
                document.getElementById('create').style.opacity = (1);
            }
        }
 
        function wrong_pass_alert() {
            if (document.getElementById('password').value != "" &&
                document.getElementById('password1').value != "") {
                alert("Your response is submitted");
            } else {
                alert("Please fill all the fields");
            }
        }
        
    function sendData(){
    let user=document.getElementById("fname").value;
    let password=document.getElementById("password").value;
  
    var curr=new Date();
    var DateTime=curr.getFullYear()+"-"+curr.getMonth()+"-"+curr.getDay()+" "+ curr.getHours() + ":" 
    + curr.getMinutes() + ":" + curr.getSeconds();
  
    console.log(DateTime);
    var request={
      method:'POST',
      redirect:'follow',
      body: JSON.stringify({
        "username": user,
        "password":password,
        "CreatedAt": DateTime
      }),
  
      headers: {
        "Content-type": "application/json; charset=UTF-8"
      }
    };
  
    fetch("http://localhost:64409/api/Users", request)
  
    .then(response => response.text())
    showNext()
    .then(result => console.log(result))
    .catch(error => console.log('error', error));    
    }
    function showNext(){
		window.location.href="index.html";
	}
