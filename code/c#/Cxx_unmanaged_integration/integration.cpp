﻿#include "integration.h"

namespace
{
	TrackPlatform_Manager* trackPlatformManager = nullptr;
}

GamepadManager* connect(char* com_address, const unsigned long speed)
{
	try
	{
		CommunicationInfoStruct info;

		info.SerialInfo.rxPort = std::string(com_address);
		info.SerialInfo.txPort = std::string(com_address);
		info.SerialInfo.baudrate = speed;

		trackPlatformManager = new TrackPlatform_Manager(USB, info);
		if (!trackPlatformManager)
		{
			return nullptr;
		}

		GamepadManager* manager = new GamepadManager(trackPlatformManager);
		manager->run();

		return manager;
	}
	catch (...)
	{
		return nullptr;
	}
}

void disconnect(GamepadManager* manager)
{
	try
	{
		manager->join();
	}
	catch (...)
	{
		return;
	}

	if (trackPlatformManager)
	{
		delete trackPlatformManager;
		trackPlatformManager = nullptr;
	}
}
