using System.Collections.Generic;
using App.Comments.Common.Entities;
using App.Comments.Common.Interfaces.Repositories;
using App.Comments.Common.Interfaces.Services;
using AutoMapper;
using System.Linq;

namespace App.Comments.Common.Services
{
    public class CommentsService : ICommentsService
    {
        public CommentsService(ICommentRepository commentRepository,
			IAuthenticationService authenticationService,
			IMapper mapper)
        {
            _commentRepository = commentRepository;
			_authenticationService = authenticationService;
			_mapper = mapper;
        }
        private readonly ICommentRepository _commentRepository;
		private readonly IAuthenticationService _authenticationService;
		private readonly IMapper _mapper;

		public IEnumerable<CommentDto> GetAllComments()
        {
			var comments = _commentRepository.GetAll().ToList();
			return _mapper.Map<List<Comment>, List<CommentDto>>(comments);
        }

        public CommentDto GetCommentByUserName(string UserName)
        {
			var commentsEntities = _commentRepository.GetCommentByUserName(UserName);
			return _mapper.Map<Comment, CommentDto>(commentsEntities);
        }

		public void AddNewComment(CommentDto comment)
		{
			Comment commentEntity =  _mapper.Map<CommentDto, Comment>(comment);
			ApplicationUser user = _authenticationService.GetUserByUserName(comment.Autor);
			if (user != null)
			{
				commentEntity.ApplicationUser = user;
				_commentRepository.AddComment(commentEntity);
			}
		}
	}
}