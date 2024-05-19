FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG USERNAME=${USER_NAME}
ARG USER_UID=1000
ARG USER_GID=$USER_UID

# Create the user
RUN groupadd --gid "$USER_GID" "$USERNAME" \
    && useradd --uid "$USER_UID" --gid "$USER_GID" -m "$USERNAME" \
    # [Optional] Add sudo support. Omit if you don't need to install software after connecting.
    && apt-get update \
    && apt-get install --no-install-recommends -y sudo \
    && echo "$USERNAME" ALL=\(root\) NOPASSWD:ALL > /etc/sudoers.d/"$USERNAME" \
    && chmod 0440 /etc/sudoers.d/"$USERNAME" \
    && apt-get clean
# ********************************************************
# * Anything else you want to do like clean up goes here *
# ********************************************************

# [Optional] Set the default user. Omit if you want to keep the default as root.
USER $USERNAME
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG USERNAME
USER $USERNAME
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY . .
# COPY ["Templates/CleanArchitecture/Devon4Net.WebAPI/Devon4Net.WebAPI.csproj", "Templates/CleanArchitecture/Devon4Net.WebAPI/"]
# COPY ["Templates/CleanArchitecture/Devon4Net.Application/Devon4Net.Application.csproj", "Templates/CleanArchitecture/Devon4Net.Application/"]
# COPY ["Templates/CleanArchitecture/Devon4Net.Infrastructure/Devon4Net.Infrastructure.csproj", "Templates/CleanArchitecture/Devon4Net.Infrastructure/"]
# COPY ["Templates/CleanArchitecture/Devon4Net.Domain/Devon4Net.Domain.csproj", "Templates/CleanArchitecture/Devon4Net.Domain/"]
RUN dotnet restore "source/Templates/CleanArchitecture/Devon4Net.WebAPI/Devon4Net.WebAPI.csproj"

# COPY . .
# COPY ./Templates/CleanArchitecture/Devon4Net.WebAPI/ /Templates/CleanArchitecture/Devon4Net.WebAPI/
# COPY ./Templates/CleanArchitecture/Devon4Net.Application/ /Templates/CleanArchitecture/Devon4Net.Application/
# COPY ./Templates/CleanArchitecture/Devon4Net.Infrastructure/ /Templates/CleanArchitecture/Devon4Net.Infrastructure/
# COPY ./Templates/CleanArchitecture/Devon4Net.Domain/ /Templates/CleanArchitecture/Devon4Net.Domain/
# COPY ["Modules", "Modules"]

WORKDIR "/src/source/Templates/CleanArchitecture/Devon4Net.WebAPI"
RUN dotnet build "Devon4Net.WebAPI.csproj" -c ${BUILD_CONFIGURATION}  --no-restore -o /app/build

FROM build AS publish
USER $USERNAME
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Devon4Net.WebAPI.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

FROM base AS final
USER $USERNAME
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devon4Net.WebAPI.dll"]
