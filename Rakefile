require "rubygems"
require "bundler/setup"
require "shellwords"

def altool_path
  return "/Applications/Xcode.app/Contents/Applications/Application\\ Loader.app/Contents/Frameworks/ITunesSoftwareService.framework/Versions/A/Support/altool"
end

namespace :build do
  desc "Codegen proper-api files"
  task :proper_api do
    command = "cd ../jigsaw-server && rm -rf tmp/csharp && docker-compose run web rake proper:api:codegen[unity,tmp/csharp]"
    puts command
    `#{command}`
  end

  desc "Build iOS project"
  task :build_ios do
    command = "rm -rf Builds"
    puts command
    `#{command}`

    command = "/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -executeMethod AutoBuilder.PerformiOSBuild"
    puts command
    `#{command}`
  end

  task :build_ipa do
    command = "cd Builds/iOS && xcodebuild archive -scheme \"Unity-iPhone\" -archivePath arc.xcarchive"
    puts command
    `#{command}`

    command = "cd Builds/iOS && xcodebuild -exportArchive -archivePath arc.xcarchive -exportPath app -exportFormat ipa -exportProvisioningProfile \"AutomationTestApp - Prod\""
    puts command
    `#{command}`
  end

  task :upload do
    command = "cd Builds/iOS && #{altool_path} --upload-app --file app.ipa -u alexey.petruchik@gmail.com -p \"@keychain:Application Loader: alexey.petruchik@gmail.com\""
    puts command
    `#{command}`
  end

  desc "Build Android project"
  task :build_android do
    command = "/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -executeMethod AutoBuilder.PerformAndroidBuild"
    puts command
    `#{command}`
  end

  desc "Cleans the whole repo to initial state"
  task :full_clean do
    command = "git reset --hard && git clean -xdf"
    puts command
    `#{command}`
  end
end