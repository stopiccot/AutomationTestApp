require "rubygems"
require "bundler/setup"
require "shellwords"
require "io/console"
require "keychain"

def altool_path
  return "/Applications/Xcode.app/Contents/Applications/Application\\ Loader.app/Contents/Frameworks/ITunesSoftwareService.framework/Versions/A/Support/altool"
end

def appleid_for_upload
  found_secret = Keychain.generic_passwords.where(:service => 'altool_password').first
  if found_secret != nil
    return found_secret.account
  end

  STDOUT.puts "We need AppleID to upload build"
  
  STDOUT.print "account:"
  account = STDIN.gets.strip
  
  STDOUT.print "password:"
  password = STDIN.noecho(&:gets).strip

  Keychain.generic_passwords.create(:service => 'altool_password', :password => password, :account => account)
  return account
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
    command = "cd Builds/iOS && #{altool_path} --upload-app --file app.ipa -u #{appleid_for_upload} -p \"@keychain:altool_password\""
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