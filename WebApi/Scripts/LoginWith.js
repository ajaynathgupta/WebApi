/// <reference path="jquery-3.3.1.min.js" />

function getAccessToken() {
    debugger;
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isUserRegistered(accessToken);
            }
        }
    }
}

function isUserRegistered(accessToken) {
    $.ajax({
        url: '/api/Account/UserInfo',
        method: 'GET',
        headers: {
            'content-type': 'application/json',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            debugger;
            if (response.HasRegistered) {
                sessionStorage.setItem('accessToken', accessToken);
                sessionStorage.setItem('userName', response.Email);
                window.location.href = 'Data.html';
            }
			else {
				signUpExternalUser(accessToken, response.LoginProvider)
            }
        }
    })
}

function signUpExternalUser(accessToken,provider) {
    $.ajax({
        url: '/api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/json',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            debugger;
			window.location.href = "/api/Account/ExternalLogin?provider="+provider+"&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A55307%2FLogin.html&state=fOsOOSErnp9dFyhOPKlMipSZRMAXTvveqDIczBUbqMA1";
        }
    })
}

