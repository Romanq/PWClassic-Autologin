#pragma once
#include <iomanip>
#include <stdio.h>
#include <thread>
#include "Functions.h"
#include <regex>
#include <iostream>
#include <string.h>
#include <comdef.h>
#pragma warning (disable: 4996)
using namespace std;



Functions f;
bool PastedPass = false;
bool SkipAgreement = false;
bool SentConfirm = false;
bool CharacterInWorld = false;
void* SavedManageEcx = NULL;
bool Stored = false;
int retries = 0;

wchar_t* charToWChar(const char* text)
{
	const size_t size = strlen(text) + 1;
	wchar_t* wText = new wchar_t[size];
	mbstowcs(wText, text, size);
	return wText;
}


DWORD __fastcall hGetWindowFunc(void* pBaseClass, void* edx, char* dialogname) {

	//printf("\r\n[WINDOW] Pointer: %p Dialog: %p", pThis, dialogname);

	/*if (GetAsyncKeyState(VK_NUMPAD4) & 0x8000) {

		//void* pCreateHero = reinterpret_cast<void*>(Functions::GetWindowFunc(pBaseClass, (char*)"Win_Create"));
		//Functions::ShowWindowFunc(pCreateHero, 1, 0, 1);

		string WindowName;
		getline(cin, WindowName);

		void* pWindow = reinterpret_cast<void*>(Functions::GetWindowFunc(pBaseClass, (char*)WindowName.c_str()));

		if(!pWindow)
			return Functions::GetWindowFunc(pBaseClass, dialogname);

		Functions::ShowWindowFunc(pWindow, 1, 0, 1);

		if (!Stored) {
			Base = pBaseClass;
			Stored = true;
		}
			

		DWORD addy = 0x005A3830;
		const char* command = "offline";
		_asm {
			push pWindow
			push command
			mov ecx, Base
			call addy
		}

	}*/
		
	retries++;

	if (retries > 1000 && !SkipAgreement) {

		void* pLoginView = reinterpret_cast<void*>(Functions::GetWindowFunc(pBaseClass, (char*)"Win_Login"));
		void* pAgreement = reinterpret_cast<void*>(Functions::GetWindowFunc(pBaseClass, (char*)"Win_LoginPage"));

		Functions::ShowWindowFunc(pAgreement, 0, 0, 1);
		Functions::ShowWindowFunc(pLoginView, 1, 0, 1);

		printf("\nAgreement: %p\nLogin: %p", pAgreement, pLoginView);

		string Password;
		string Login;

		auto line = wstring(GetCommandLineW());
		string Definedstr = string(line.begin(), line.end());

		regex regexp(R"([^\-]+|[^\-s]+)");
		smatch m;

		string::const_iterator searchStart(Definedstr.cbegin());

		int match = 0;
		while (regex_search(searchStart, Definedstr.cend(), m, regexp))
		{
			cout << (searchStart == Definedstr.cbegin() ? "" : " ") << m[0];
			searchStart = m.suffix().first;



			if (match == 1) {
				Login = m[0].str();
				std::string::iterator end_pos = remove(Login.begin(), Login.end(), ' ');
				Login.erase(end_pos, Login.end());
			}

			if (match == 2) {
				Password = m[0].str();
			}
			match++;
		}

		UIObject* pPassword = Functions::GetDialogFunc(pLoginView, (char*)"Txt_PassWord");
		UIObject* pLogin = Functions::GetDialogFunc(pLoginView, (char*)"DEFAULT_Txt_Account");

		if (pPassword && pPassword != NULL && pLogin && !PastedPass) {


			wchar_t* Pass = charToWChar(Password.c_str());
			wchar_t* Log = charToWChar(Login.c_str());

			pPassword->SetText((char*)Pass);
			pLogin->SetText((char*)(Log));

			if (pLoginView && pLoginView != NULL && !PastedPass) {

				UIObject* pButton = Functions::GetDialogFunc(pLoginView, (char*)"Btn_Game");

				printf("\r\nESI: %p\r\nEBP: %p\r\nECX: %p", pLoginView, pButton, pBaseClass);

				PastedPass = true;

				/*
					Window Ptr
					Button Ptr of Window
					pThis of Window
				*/

				DWORD addy = 0x005A3830;
				const char* command = "confirm";
				_asm {
					push pLoginView
					push command
					mov ecx, pBaseClass
					call addy
				}

				pLoginView = NULL;
			}

		}

		SkipAgreement = true;
		SentConfirm = true;
		retries = 0;
		
	}
	
		/*_asm {		ShowWindow asm function
			push 0x1
			push 0x0
			push 0x1
			mov ecx, res
			call show
		}*/


	if (strcmp(dialogname, "Win_Select") == 0 && SkipAgreement && SentConfirm && !CharacterInWorld && retries > 2500) {

		//¬ход на последнего персонажа
		void* pCharacterList = reinterpret_cast<void*>(Functions::GetWindowFunc(pBaseClass, (char*)"Win_Manage"));

		if (pCharacterList && pCharacterList != NULL) {

			//printf("\r\npCharacterList %p CharSelect %p", pCharacterList, pBaseClass);

			DWORD addy = 0x005A3E90;
			const char* command = "confirm";
			_asm {
				push pCharacterList
				push command
				mov ecx, pBaseClass
				call addy
			}

		}

		CharacterInWorld = true;
	}

	//printf("\r\n[WINDOW] %s", dialogname);

	return Functions::GetWindowFunc(pBaseClass, dialogname);
}

