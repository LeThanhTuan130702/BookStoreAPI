﻿using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Model;

namespace BookStoreAPI.Helpers
{
    public class ApplicationMapper: Profile
    {
        public ApplicationMapper() {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}