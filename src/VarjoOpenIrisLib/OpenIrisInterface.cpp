#include "OpenIrisInterface.h"


// Application instance
std::unique_ptr<IApplication> g_application;


// Callback for handling Ctrl+C
BOOL WINAPI CtrlHandler(DWORD dwCtrlType)
{
    if (g_application) {
        g_application->terminate();
    }
    return TRUE;
}


// Command line parsing helpers
varjo_ChannelFlag parseChannels(const std::string& str);

std::string getAppNameAndVersionText() { return std::string("Varjo Eye Tracking Camera Example ") + varjo_GetVersionString(); }

std::string getCopyrightText() { return "(C) 2022-2024 Varjo Technologies"; }

// Define a function pointer type for the callback

//CallbackType openIris_callback = nullptr;

int MyFunction2()
{
    std::vector<uint8_t> frame = { 1, 2, 3, 4, 5 };
    FrameInfo frameInfo{};
    frameInfo.frameIndex = 123456;  // Example frame index
    frameInfo.timestamp = 987654;  // Example timestamp
    uint8_t* frameData = static_cast<uint8_t*>(malloc(frame.size()));

    if (frameData) {
        memcpy(frameData, frame.data(), frame.size());
    }

    int size = static_cast<int>(frame.size());
   // if (g_application->openIris_callback) {
   //     bool res = g_application->openIris_callback(frameData, size, frameInfo); // Example: Call the callback with a value
   // }
    //MyFunction(0, 0);
    return 2;
}

int GetLastFrame(uint8_t** frameData, int* size, FrameInfo* frameInfo) {
    // Example frame data and frame number
    std::vector<uint8_t> frame = { 1, 2, 3, 4, 5 }; // Example frame data
    int64_t lastFrameNumber = 123456789;          // Example frame number

    frameInfo->frameIndex = 123456;  // Example frame index
    frameInfo->timestamp = 987654;  // Example timestamp

    *size = static_cast<int>(frame.size());
    // Allocate memory and copy the frame data
    *frameData = static_cast<uint8_t*>(malloc(frame.size()));
    if (*frameData) {
        memcpy(*frameData, frame.data(), frame.size());
    }
    return 0;
}

// Function to free the allocated frame data
int FreeFrameData(uint8_t* frameData) {
    if (frameData) {
        free(frameData);
    }
    return 0;
}


int VarjoStartCameras(CallbackType callback) {
    int argc = 1;
    char* argv[] = { "Hello" };
    /* DLL loaded dynamically
    // Load the DLL
    HMODULE hDll = LoadLibraryA("EyeCameraStreamExampleDLL.dll");
    if (!hDll) {
        std::cerr << "Failed to load DLL" << std::endl;
        return 1;
    }

    // Get the function address
    MYFUNC MyFunction = (MYFUNC)GetProcAddress(hDll, "MyFunction");
    if (!MyFunction) {
        std::cerr << "Failed to get function address" << std::endl;
        FreeLibrary(hDll);
        return 1;
    }

    // Call the function
    int result = MyFunction(2, 3);
    std::cout << "Result: " << result << std::endl;

    // Clean up
    FreeLibrary(hDll);
    */

    cxxopts::Options options("EyeTrackingCameraStreamExample", getAppNameAndVersionText() + "\n" + getCopyrightText());
    options.add_options()  //
        ("channels", "Which channels to output. Defaults to 'both'. Allowed options are 'left', 'right' and 'both'.",
            cxxopts::value<std::string>()->default_value("both"))  //
        ("streaming", "Run streaming FPS test instead default UI application.",
            cxxopts::value<bool>()->default_value("false"))  //
        ("help", "Display help info");

    IApplication::Options appOptions;
    try {
        // Parse command line arguments
        auto arguments = options.parse(argc, argv);
        if (arguments.count("help")) {
            std::cout << options.help();
            return EXIT_SUCCESS;
        }
        appOptions.channels = parseChannels(arguments["channels"].as<std::string>());
    }
    catch (const std::exception& e) {
        std::cerr << e.what();
        return EXIT_FAILURE;
    }

    // Setup Ctrl+C handler to exit application cleanly
    SetConsoleCtrlHandler(CtrlHandler, TRUE);

    try {
        // Initialize session
        auto session = std::make_shared<Session>();
        if (!session->isValid()) {
            throw std::runtime_error("Failed to initialize session. Is Varjo system running?");
        }

        // Initialize application
        g_application = std::make_unique<StreamingApplication>(std::move(session), appOptions);

        LOG_INFO(
            "%s\n"
            "%s\n"
            "-------------------------------",
            getAppNameAndVersionText().c_str(), getCopyrightText().c_str());

        g_application->openIris_callback = callback;

        // Execute application
        g_application->run();

        // Application finished
        return EXIT_SUCCESS;
    }
    catch (const std::exception& e) {
        std::cerr << "Critical error caught: " << e.what();
        return EXIT_FAILURE;
    }


    return 22; // Example: a simple addition
}

int VarjoStop() {
    g_application->terminate();
    return 0;
}

std::string toLower(std::string s)
{
    std::transform(s.begin(), s.end(), s.begin(), [](unsigned char c) { return std::tolower(c); });
    return s;
}

varjo_ChannelFlag parseChannels(const std::string& str)
{
    const auto lcStr = toLower(str);
    if (lcStr == "left") {
        return varjo_ChannelFlag_First;
    }
    else if (lcStr == "right") {
        return varjo_ChannelFlag_Second;
    }
    else if (lcStr == "both") {
        return varjo_ChannelFlag_First | varjo_ChannelFlag_Second;
    }

    throw std::runtime_error("Unsupported command line option --channels=" + str);
}


