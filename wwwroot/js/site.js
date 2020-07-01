
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const url = "https://localhost:44359/api/Users/";
const urlGames = "https://localhost:44359/api/Games/";


function choosenAvatar(id) {
	document.getElementById("avatar").value = id
	document.getElementById("avatarImg").src = id
}


function signUp() {
	
	const formData = new FormData();

	var email = document.getElementById("email").value;
	var nickName = document.getElementById("nickName").value;
	var password = document.getElementById("password").value;
	var avatar = document.getElementById("avatar").value;

	formData.append("email", email);
	formData.append("nickName", nickName);
	formData.append("password", password);
	formData.append("avatar", avatar);

	fetch(`${url}`, {
		method: 'POST',		
		body: formData
	})
		.then(response => response.text())			
		.then(data => {
			document.getElementById("response").innerText = data;
		})
		.catch(error => console.error('Unable to get Account.', error));
		
}

function edit() {
	
	const formData = new FormData();
	var id = document.getElementById("userid").value;
	var email = document.getElementById("email").value;
	var nickName = document.getElementById("nickName").value;
	var password = document.getElementById("password").value;
	var avatar = document.getElementById("avatar").value;

	formData.append("email", email);
	formData.append("nickName", nickName);
	formData.append("password", password);
	formData.append("avatar", avatar);

	fetch(`${url}`+id, {
		method: 'PUT',		
		body: formData
	})
		.then(response => response.text())			
		.then(data => {
			localStorage.clear();
			localStorage.setItem("user", data);
			var user = JSON.parse(data);
			document.getElementById("userid").value = user.UserId;
			document.getElementById("email").value = user.Email;
			document.getElementById("nickName").value = user.Username;
			document.getElementById("password").value = user.Password;
			document.getElementById("avatarImg").src = user.Avatar;
			document.getElementById("avatar").value = user.Avatar;
		})
		.catch(error => console.error('Unable to get Account.', error));
		
}


function signIn() {
	const formData = new FormData();

	var email = document.getElementById("signInEmail").value;
	var password = document.getElementById("signInPassword").value;

	formData.append("signInEmail", email);
	formData.append("signInPassword", password);

	fetch(`${url}`, {
		method: 'PUT',
		body: formData
	})
		.then(response => response.text())
		.then(data => {
			localStorage.setItem("user", data);
			var user = JSON.parse(data);
			var role = user.Role;			
			if (!role) {
				window.location.href = "/Home/MyPage";
			} else {
				window.location.href = "/Home/Admin";

			}

		})
		.catch(error => console.error('Unable to get Account.', error));

}
function CreateGame() {		
	var paintName = prompt("What are you going to draw?");

	if (paintName == null || paintName == "") {

		window.alert("Please Enter A Paint Name!");
		
	} else {
		var formData = new FormData();
		var user = JSON.parse(localStorage.getItem('user'));
		var userId = user.UserId;


		formData.append("userid", userId);
		formData.append("drawText", paintName);

		fetch(`${urlGames}`, {
			method: 'POST',
			body: formData
		})
			.then(response => response.text())
			.then(data => {
				window.location.href = "/Home/TheGame";
			})
			.catch(error => console.error('Unable to get Account.', error));
		
		
		

	}
}


function DeleteGame(id) {
	var delUrl = urlGames + "Delete/" + id;
	fetch(delUrl, {
		method: 'delete'
	})
		.then(response => response.text())
		.then(data => {
			window.location.href = "/Home/Admin";
		})
		.catch(error => console.error('Unable to get Account.', error));
}

function DeleteUser(id) {
	var delUrl = url + "Delete/" + id;
	fetch(delUrl, {
		method: 'delete'
	})
		.then(response => response.text())
		.then(data => {
			window.location.href = "/Home/Admin";
		})
		.catch(error => console.error('Unable to get Account.', error));
}

function ActiveGame() {
	
	window.location.href = "/Home/TheGame";

}