FROM ubuntu:18.04

# Install Pre-requisites
RUN apt-get update \
    && apt-get -y upgrade \
    && apt-get -y install gnupg wget
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
    && echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" | tee /etc/apt/sources.list.d/mono-official-stable.list \
    && apt-get update
ENV DEBIAN_FRONTEND=noninteractive
RUN apt-get install -y tzdata \
    && ln -fs /usr/share/zoneinfo/America/New_York /etc/localtime \
    && dpkg-reconfigure --frontend noninteractive tzdata
RUN apt-get -y install nuget mono-complete
ADD . /build
# Build
RUN cd /build \
    && wget https://dist.nuget.org/win-x86-commandline/latest/nuget.exe \
    && mono nuget.exe restore apprise-mobile-csharp.sln \
    && xbuild /p:Configuration=Release apprise-mobile-csharp.sln